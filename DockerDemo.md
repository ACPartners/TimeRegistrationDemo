# Docker Demo guide

## Setup

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
To initialize the database, we initially needed to change the connectionstring.
When running the application, the container with the database is called demodb.
When running the migrations command to update the database, the server is localhost.
You could try to set up an reverse proxy to let demodb point to localhost to avoid the need to change this.
We just let the port from the container be forwared to localhost. If you don't have a local db running, this is no problem!


## Practical problems encountered

### Reaching the container from the browser

This is an issue for developers more then admins.
When developing on your machine, you are not in the same network as the one which hosts the containers.
At least not when running docker-compose yourself.

You need to forward the ports on the containers to relevant ports on localhost.


### IdentityServer  has a different name depending on the address you call it from

Identityserver takes the hostname as part of its  Issuer name on the security tokens.
This means that on localhost we see  htttp://localhost:5000.
Inside the container network they see http://quickidentityserver

This gave us tokens that our application back-end refused but it was talking to identityserver on a different hostname!

### Launching the database with the build every time

Initially we added the creation and launch of the database container to the docker-compose file.
This lead to some unstable behaviors we didn't really to simulate all the time.
Sometimes the database took too long to setup, which caused timeouts in our back-end.
For some reason, a different container was made every time we started debugging.

we now manually start the database container, this also means it doesn't stop between debugging sessions like the app-containers!

### Security tokens from the future

For some reason, the time inside the container for identityserver became different from the system host time.
The date was still correct, but the clock was running a couple of hours in the future.
All the proposed remedies online were not working or not easily done.

Restart docker didn't work
Setting up a time sync server is beyond our scope we wanted to take on.

Finally we removed the image of identityserver and let it be re-created again by the Visual Studio build.
Don't remove the underlying  aspnet/core image!!

## Starting the containers by yourselves

 Starting command for  the containers:
 docker-compose  -f "C:\Users\Stijn\Source\Repos\TimeRegistrationDemo\Code\TimeRegistrationDemo\docker-compose.yml" -f "C:\Users\Stijn\Source\Repos\TimeRegistrationDemo\Code\TimeRegistrationDemo\docker-compose.override.yml" -f "C:\Users\Stijn\Source\Repos\TimeRegistrationDemo\Code\TimeRegistrationDemo\obj\Docker\docker-compose.vs.debug.g.yml" -p dockercompose18376638957051028791 up -d

