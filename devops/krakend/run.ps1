docker container stop krakend
docker container remove krakend
docker run -p 8080:8080 --name krakend --detach -v $PWD/krakend-volume:/etc/krakend/ devopsfaith/krakend:watch run --config /etc/krakend/krakend.json
docker exec krakend krakend check --config /etc/krakend/krakend.json    # check config is correct
docker exec krakend krakend audit --config /etc/krakend/krakend.json    # recommentations
docker logs krakend --follow