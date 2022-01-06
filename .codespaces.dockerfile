FROM mcr.microsoft.com/vscode/devcontainers/base:debian

USER vscode

# Install .NET SDK
# Source: https://docs.microsoft.com/dotnet/core/install/linux-scripted-manual#scripted-install
RUN mkdir -p /home/vscode/dotnet && curl -fsSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --install-dir /home/vscode/dotnet -c Current
ENV DOTNET_ROOT=/home/vscode/dotnet
ENV PATH=$PATH:/home/vscode/dotnet
