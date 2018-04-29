# Docker Demo Setup guide

To able to run the demo:  
Launch the containers  
-> Visual Studio  
-> Open solution of TimeRegistrationDemo  
-> Set docker project as starter project  
-> Start Debug session ( first time the containers will be downloaded and built)  
during debug -> docker network inspect nat  (retrieve the ip addresses of the containers)  
we have the containers running with an known name.  (bad practise in production)

Unfortunately the host machine doesn't recieve those names because he is not part of the network group of containers.  
We can change the hosts file to point to the containers' ip addresses!

In earlier testing this resulted in the following lines in my hosts file:  
172.18.51.75 demodb  
172.18.60.150 quickstartidentityserver  
172.18.51.215 timeregistrationdemo.webapi  

After saving, we can surf and browse to our demo site with this url: [http://timeregistrationdemo.webapi/swagger/](http://timeregistrationdemo.webapi/swagger/)  
Also the database is then reachable through demodb instead of the ipaddress

Attention:  
If you use the demo from the url
