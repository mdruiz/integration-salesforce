# DEFINITION - VAGRANT
Vagrant.require_version "= 2.0.2"
Vagrant.configure("2") do |co|

# DEFINITION - CONFIGURATION
  co.vm.box = "ubuntu/xenial64"
  co.vm.box_check_update = false
  co.vm.box_version = "20180309.0.0"
  co.vm.hostname = "dev.vagrant.box"
  co.vm.post_up_message = "run 'cd /vagrant' to access the project artifacts."
  co.vm.provision "shell", path: "vagrantup.sh"
  co.vm.synced_folder ".", "/vagrant"

# DEFINITION - NETWORK
  co.vm.network "forwarded_port", guest: 9000, host: 9000, host_ip: "127.0.0.1" # UIX
  co.vm.network "forwarded_port", guest: 9200, host: 9200, host_ip: "127.0.0.1" # API
  co.vm.network "forwarded_port", guest: 9400, host: 9400, host_ip: "127.0.0.1" # BIZ
  co.vm.network "forwarded_port", guest: 9600, host: 9600, host_ip: "127.0.0.1" # CTX
  co.vm.network "forwarded_port", guest: 9800, host: 9800, host_ip: "127.0.0.1" # DSR

# DEFINITION - PROVIDER
  co.vm.provider "virtualbox" do |vb|
    vb.gui = false
    vb.name = "vagrantbox-#{Time.now.to_i}"
    vb.memory = 2048
  end
end
