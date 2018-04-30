# Docker Essentials

## Basics


You make a image with docker compose
You create a container running that image
You compose a service

## docker database sqlexpress
download the public image like this :
docker pull microsoft/mssql-server-windows-express
run an instance like this ( password must be complex enough, otherwise login will fail, no other warning!)
docker run -d -p 1433:1433 -e sa_password=azurePASSWORD123 -e ACCEPT_EULA=Y microsoft/mssql-server-windows-express
connect to the instance like this
docker exec -it <DOCKER_CONTAINER_ID> sqlcmd
or
connect from SSMS:
figure out ipaddress
docker inspect –format="{{range .NetworkSettings.Networks}}{{.IPAddress}}{{end}}" <DOCKER_CONTAINER_ID>

use it in SSMS

## Docker commands

### List Docker CLI commands
docker
docker container --help

### Display Docker version and info
docker --version
docker version
docker info

### Execute Docker image
docker run hello-world

### List Docker images
docker image ls

### List Docker containers (running, all, all in quiet mode)
docker container ls
docker container ls --all
docker container ls -aq

### Launch Docker swarm and start service

docker swarm init
docker stack deploy -c docker-compose.yml getstarted
docker stack rm getstarted  // stop the service

docker swarm leave --force  // take down the swarm


If you change the docker.compose.yml file and save,
you can run the deploy command again, docker will handle the changes like start up more replica's if requested.


### work with services

docker service ls
docker service ps getstartedlab_web   // Send ps command to linux host


docker ps --filter "status=running" --filter "name=demodb" --format {{.ID}} -n 1
docker inspect --format="{{.NetworkSettings.Networks.nat.IPAddress}}" d5d


## Contents of a docker file

WORKDIR 
ADD
RUN
EXPOSE
CMD

## Example docker file

### Use an official Python runtime as a parent image
FROM python:2.7-slim

### Set the working directory to /app
WORKDIR /app

### Copy the current directory contents into the container at /app
ADD . /app

### Install any needed packages specified in requirements.txt
RUN pip install --trusted-host pypi.python.org -r requirements.txt

### Make port 80 available to the world outside this container
EXPOSE 80

### Define environment variable
ENV NAME World

### Run app.py when the container launches
CMD ["python", "app.py"]


## Componse the service with a docker-compose.yml

version: "3"
services:
  web:
    # replace username/repo:tag with your name and image details
    image: username/repo:tag
    deploy:
      replicas: 5
      resources:
        limits:
          cpus: "0.1"
          memory: 50M
      restart_policy:
        condition: on-failure
    ports:
      - "80:80"
    networks:
      - webnet
networks:
  webnet:
