#!/bin/bash

# PACKAGE - VERSION
VERSION_DOCKER_CE=17.09.0~ce-0~ubuntu
VERSION_DOCKER_COMPOSE=1.17.0
VERSION_DOCKER_MACHINE=v0.13.0

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
  docker-ce=$VERSION_DOCKER_CE
