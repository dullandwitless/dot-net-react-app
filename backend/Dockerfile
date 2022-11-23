# syntax=docker/dockerfile:1.4

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:6.0 AS base

WORKDIR /src

COPY TTEC.EMPOWER.* ./
# RUN ["dotnet", "restore"]

FROM base as builder

COPY . .

CMD ["dotnet", "build", "./EMPOWER.sln", "-o", "/build"]

FROM builder as dev-envs

RUN <<EOF
apt-get update
apt-get install -y git
EOF

RUN <<EOF
useradd -s /bin/bash -m vscode
groupadd docker
usermod -aG docker vscode
EOF
# install Docker tools (cli, buildx, compose)
COPY --from=docker/compose / /

CMD ["dotnet", "run"]

FROM builder AS publisher

RUN ["dotnet", "publish", "-c", "Debug",  "./EMPOWER.sln", "-o", "/build"]

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/aspnet:6.0

WORKDIR /app
COPY --from=publisher /build .

CMD ["dotnet", "TTEC.EMPOWER.API.dll"]