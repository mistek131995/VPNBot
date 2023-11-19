FROM mcr.microsoft.com/dotnet/sdk
USER root
RUN apt-get update && mkdir /home/source && mkdir /home/build
ADD . /home/source
RUN cd /home/source && dotnet publish -o /home/build