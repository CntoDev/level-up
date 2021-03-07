docker container stop my-identity
docker container rm my-identity
# docker run --name my-identity -p 443:443 --volume C:\cnto\certificate\dev:/cert/ -it --entrypoint /bin/sh  cnto/identity:0.9
docker run --name my-identity -p 443:443 -p 80:80 --volume C:\cnto\certificate\dev:/cert/ -e IdentityCertificatePwd=cnto-development cnto/identity:0.9