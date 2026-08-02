import unittest
from flask import Flask
from sqlalchemy import create_engine
from sqlalchemy.orm import sessionmaker

from api.legacy_resource_decimal_bridge import install_legacy_resource_bridge
from api.models import Base, Project, Resource
from api.resource_decimal import ResourceDecimalService


class LegacyResourceDecimalBridgeTests(unittest.TestCase):
    def setUp(self):
        self.engine=create_engine("sqlite+pysqlite:///:memory:",future=True)
        Base.metadata.create_all(self.engine)
        self.sessions=sessionmaker(bind=self.engine)
        db=self.sessions();db.add(Project(id=1,code="P1",name="專案",owner_id=1));db.commit();db.close()
        self.app=Flask(__name__)
        self.app.add_url_rule("/api/projects/<int:project_id>/resources/","list_resources",lambda project_id,user_id:None,methods=["GET"])
        self.app.add_url_rule("/api/projects/<int:project_id>/resources/","create_resource",lambda project_id,user_id:None,methods=["POST"])
        self.app.add_url_rule("/api/projects/<int:project_id>/resources/<int:resource_id>","update_resource",lambda project_id,resource_id,user_id:None,methods=["PUT"])
        install_legacy_resource_bridge(self.app,self.engine,self.sessions)

    def test_create_dual_writes_legacy_and_decimal(self):
        with self.app.test_request_context(json={"code":"R1","c_name":"材料","c_unit":"kg","unit_price":"12.3456"}):
            response,status=self.app.view_functions["create_resource"](1,1)
        self.assertEqual(201,status)
        payload=response.get_json()
        self.assertEqual("12.3456",payload["unit_price"])
        self.assertTrue(payload["decimal_core"])
        db=self.sessions();legacy=db.query(Resource).filter(Resource.id==payload["id"]).first();db.close()
        self.assertIsNotNone(legacy)
        shadow=ResourceDecimalService(self.engine).get_resource(f"legacy-resource-{payload['id']}")
        self.assertEqual("12.3456",shadow["unit_price"])

    def test_update_uses_row_version_and_decimal_response(self):
        with self.app.test_request_context(json={"code":"R1","c_name":"材料","unit_price":"10"}):
            response,status=self.app.view_functions["create_resource"](1,1)
        created=response.get_json()
        with self.app.test_request_context(json={"unit_price":"20.005","row_version":created["row_version"],"propagate":False}):
            response=self.app.view_functions["update_resource"](1,created["id"],1)
        payload=response.get_json()
        self.assertEqual("20.0050",payload["unit_price"])
        self.assertEqual(created["row_version"]+1,payload["row_version"])

    def test_stale_update_does_not_change_legacy_price(self):
        with self.app.test_request_context(json={"code":"R1","c_name":"材料","unit_price":"10"}):
            response,status=self.app.view_functions["create_resource"](1,1)
        created=response.get_json()
        with self.app.test_request_context(json={"unit_price":"20","row_version":created["row_version"],"propagate":False}):
            self.app.view_functions["update_resource"](1,created["id"],1)
        with self.app.test_request_context(json={"unit_price":"99","row_version":created["row_version"],"propagate":False}):
            response,status=self.app.view_functions["update_resource"](1,created["id"],1)
        self.assertEqual(409,status)
        db=self.sessions();legacy=db.query(Resource).filter(Resource.id==created["id"]).first();self.assertEqual(20.0,legacy.unit_price);db.close()


if __name__=="__main__":unittest.main()
