# Hona Teaser

Watch a short teaser of our final result!
https://www.youtube.com/watch?v=UA2LOtBUgtg

# Install

This will require a few GB of free space on your pc.

## 1. Local dependencies

Install git (https://git-scm.com/downloads). 
Install docker (https://docs.docker.com/get-docker/).
With Windows it might be necessary to install wsl2 to use docker.

Open a console in the folder where you want to install the backend.
Download repository with the command 'git clone https://github.com/Honduran-Emerald/backend'.

## 2. Backend dependencies

The following environment variables need to be filled in the Docker Compose file "docker-compose.yml" in the root directory (a normal texteditor like notepad can be used to open).
Enter them in the fields marked with <...> (example: change KissLog__OrganizationId: "<>" to "xyz"):

Setup Kisslog (https://kisslog.net), create an organization and application and fill both ids in Docker Compose (this can take a bit longer).
Setup JwtKey by entering a secure password of your choice for encryption of authentication tokens.

## 3. Run

Start a console in the root directory and run 'docker-compose build' and 'docker-compose up' (this can take a few minutes).
The backend should be running at Port 812 and can now be accessed though the browser at 'http://localhost:812/swagger'.
