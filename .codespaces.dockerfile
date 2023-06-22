FROM mcr.microsoft.com/vscode/devcontainers/base:debian

USER vscode

# Install dependencies
RUN \
    sudo apt-get update && export DEBIAN_FRONTEND=noninteractive        \
    && sudo apt-get -y install --no-install-recommends xauth mesa-utils \
    && sudo apt-get autoremove -y                                       \
    && sudo apt-get clean -y                                            \
    && sudo rm -rf /var/lib/apt/lists/*

# Install .NET SDK
# Source: https://docs.microsoft.com/dotnet/core/install/linux-scripted-manual#scripted-install
RUN \
  mkdir -p /home/vscode/dotnet                                                                                  \
  && curl -fsSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --install-dir /home/vscode/dotnet -c STS

# .NET Environment Variables
ENV DOTNET_ROOT=/home/vscode/dotnet
ENV DOTNET_NOLOGO=true
ENV DOTNET_SKIP_FIRST_TIME_EXPERIENCE=true

# Path
ENV PATH=$PATH:/home/vscode/dotnet