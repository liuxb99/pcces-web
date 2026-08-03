FROM golang:1.24-bookworm AS build
WORKDIR /src
COPY go.mod go.sum ./
RUN go mod download
COPY . .
RUN CGO_ENABLED=0 GOOS=linux go build -trimpath -o /out/pcces-server ./cmd/pcces-server

FROM gcr.io/distroless/static-debian12:nonroot
WORKDIR /tmp
COPY --from=build /out/pcces-server /usr/local/bin/pcces-server
EXPOSE 8080
ENTRYPOINT ["/usr/local/bin/pcces-server"]
CMD ["-db", "/tmp/pcces-local.db", "-listen", "0.0.0.0:8080"]
