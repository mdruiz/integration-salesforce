#!/bin/bash

# PACKAGE - VERSION
DOCKER_CE_VERSION=17.12.0~ce-0~ubuntu
DOCKER_CT_VERSION=1.18.0

# PACKAGE - DEFINITION
apt update
apt install -y \
  apt-transport-https \
  ca-certificates \
  curl \
  software-properties-common

# DOCKER - REPOSITORY
curl -fsSL https://download.docker.com/linux/ubuntu/gpg | sudo apt-key add -
add-apt-repository \
  "deb [arch=amd64] https://download.docker.com/linux/ubuntu \
  $(lsb_release -cs) stable"

# DOCKER - COMMUNITY EDITION
apt update
apt install -y \
  docker-ce=$DOCKER_CE_VERSION

# DOCKER - COMPOSE TOOLING
apt update
curl -L https://github.com/docker/compose/releases/download/$DOCKER_CT_VERSION/docker-compose-`uname -s`-`uname -m` \
  -o /usr/local/bin/docker-compose
chmod +x /usr/local/bin/docker-compose

# DOCKER - PERMISSION
usermod -aG docker vagrant
shutdown -r 0
