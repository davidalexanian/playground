docker pull wiremock/wiremock:latest

docker container remove wiremock
docker volume remove wiremock-volume

docker run -it --rm `
    -p 8090:8080 `
    -v $pwd/wiremock-volume:/home/wiremock `
    --name wiremock `
    wiremock/wiremock:latest --verbose

sleep 
curl GET http://localhost:8090/__admin
curl GET http://localhost:8090/hello
curl GET http://localhost:8090/sample
curl GET http://localhost:8090/non-existing