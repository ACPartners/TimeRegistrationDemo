# Docker Demo Setup guide
 

 
To able to run the demo:  
 
Launch the containers  
 
-> Visual Studio  
 
-> Open solution of TimeRegistrationDemo  
 
-> Set docker project as starter project  
 
-> Start Debug session ( first time the containers will be downloaded and built)  
 
during debug -> docker network inspect nat  (retrieve the ip addresses of the containers)  
 
The containers can talk to each other through the service names we provide in the docker_compose.yml file

Unfortunately the host machine doesn't recieve those names because he is not part of the network group of containers.  
 
How can we reach the services then? In production environments, a proxy DNS handler would interact with the docker management service api to discover the services and provide dns address resolving for them.
In development we map the ports from the containers to the localhost to test!

TimeregistrationDemo.WebApi :  http://localhost:3000 
QuickstartIdentityServer    :  http://localhost:5000
Sql database                :  http://localhost:1337  ( default port for sql server, can conflict with existing!!)

Attention:
To initialize the database, you need to change the connectionstring.
When running the application, the container with the database is called demodb.
When running the migrations command to update the database, the server is localhost.
You could set up an reverse proxy to let demodb point to localhost to avoid the need to change this.
  

 Starting command for  the containers:
 docker-compose  -f "C:\Users\Stijn\Source\Repos\TimeRegistrationDemo\Code\TimeRegistrationDemo\docker-compose.yml" -f "C:\Users\Stijn\Source\Repos\TimeRegistrationDemo\Code\TimeRegistrationDemo\docker-compose.override.yml" -f "C:\Users\Stijn\Source\Repos\TimeRegistrationDemo\Code\TimeRegistrationDemo\obj\Docker\docker-compose.vs.debug.g.yml" -p dockercompose18376638957051028791 up -d

