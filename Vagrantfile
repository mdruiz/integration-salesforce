# DEFINITION - VAGRANT
Vagrant.require_version ">= 2.0.1", "< 2.1.0"
Vagrant.configure("2") do |co|

# DEFINITION - BOX
  co.vm.box = "ubuntu/xenial64"
  co.vm.box_version = "20171024.0.0"

# DEFINITION - NETWORK
  co.vm.network "forwarded_port", guest: 7000, host: 7000, host_ip: "127.0.0.1" # uix
  co.vm.network "forwarded_port", guest: 8000, host: 8000, host_ip: "127.0.0.1" # api
  co.vm.network "forwarded_port", guest: 9000, host: 9000, host_ip: "127.0.0.1" # dal

# DEFINITION - PROVISION
  co.vm.provision "shell", path: "Vagrantup.sh"
end
