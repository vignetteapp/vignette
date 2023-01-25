FROM mcr.microsoft.com/vscode/devcontainers/base:debian

USER vscode

# Install Xvfb
RUN sudo apt-get update && export DEBIAN_FRONTEND=noninteractive \
    && sudo apt-get -y install --no-install-recommends \
        xvfb xauth \
    && sudo apt-get autoremove -y \
    && sudo apt-get clean -y \
    && sudo rm -rf /var/lib/apt/lists/*

# Install .NET SDK
# Source: https://docs.microsoft.com/dotnet/core/install/linux-scripted-manual#scripted-install
RUN mkdir -p /home/vscode/dotnet && \
  curl -fsSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --install-dir /home/vscode/dotnet -c STS && \
  curl -fsSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --install-dir /home/vscode/dotnet -c LTS

ENV DOTNET_ROOT=/home/vscode/dotnet
ENV PATH=$PATH:/home/vscode/dotnet