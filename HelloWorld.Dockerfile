FROM ubuntu:latest

RUN apt-get update && apt-get install vim --yes

ENTRYPOINT [ "bash" ]