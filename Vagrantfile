# DEFINITION - VAGRANT
Vagrant.require_version ">= 2.0.1", "< 2.1.0"
Vagrant.configure("2") do |co|

# DEFINITION - BOX
  co.vm.box = "ubuntu/xenial64"
  co.vm.box_version = "20171024.0.0"

# DEFINITION - PROVISION
  co.vm.provision "shell", path: "Vagrantup.sh"
end
