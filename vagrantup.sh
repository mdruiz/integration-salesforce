#!/bin/bash

# PACKAGE - VERSION
DOCKER_CE_VERSION=17.12.0~ce-0~ubuntu

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

# DOCKER - PERMISSION
usermod -aG docker $(whoami)
shutdown 0
