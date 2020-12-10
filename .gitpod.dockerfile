FROM gitpod/workspace-full-vnc

USER root

RUN wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb && \
    dpkg -i packages-microsoft-prod.deb && \
    apt update && \
    apt-get install -y dotnet-sdk-5.0