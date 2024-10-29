#!/bin/bash

#Create the Pod
kubectl apply -f https://k8s.io/examples/application/shell-demo.yaml
kubectl get pod shell-demo
kubectl exec --stdin --tty shell-demo -- /bin/bash
# Run this inside the container
ls /
