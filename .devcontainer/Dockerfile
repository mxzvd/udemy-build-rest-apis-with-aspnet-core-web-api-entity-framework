FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine

RUN apk update && \
    apk add --no-cache \
    openssh

EXPOSE 8080
EXPOSE 8090

CMD ["bash"]
