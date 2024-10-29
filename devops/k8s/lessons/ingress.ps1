#create 3 deployments each with 3 pods and one service attached to each of 3-pods
kubectl create deployment be-default --image=gcr.io/kuar-demo/kuard-amd64:blue --replicas=3 --port=8080
kubectl expose deployment be-default
kubectl get services -o wide
kubectl get pod -l alpaca

#create ingress with rule: map alpaca-local/any_path to service alpaca on port 8080
kubectl create ingress alpaca-localhost --class=nginx --rule="alpaca.local/*=alpaca:8080"

#port forward to ingress-nginx-controller service (not the alpaca service)
kubectl port-forward --namespace=ingress-nginx service/ingress-nginx-controller 8080:80

#curl
curl http://alpaca.local:8080/