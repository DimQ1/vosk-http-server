#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["VoskApi/VoskApi.csproj", "VoskApi/"]
RUN dotnet restore "VoskApi/VoskApi.csproj"
COPY ./VoskApi ./VoskApi
WORKDIR "/src/VoskApi"
RUN dotnet build "VoskApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VoskApi.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
# Workaround https://unix.stackexchange.com/questions/2544/how-to-work-around-release-file-expired-problem-on-a-local-mirror

RUN echo "Acquire::Check-Valid-Until \"false\";\nAcquire::Check-Date \"false\";" | cat > /etc/apt/apt.conf.d/10no--check-valid-until
RUN apt-get update && apt install unzip
RUN apt install libatomic1
COPY ./vosk-model-ru-*.zip .
ENV RUVERSION 0.22
RUN dir
RUN unzip vosk-model-ru-${RUVERSION}.zip \
   && mv vosk-model-ru-${RUVERSION} TextModel \
   && rm -rf model/extra \
   && rm -rf model/rescore/G.carpa \
   && rm -rf vosk-model-ru-${RUVERSION}.zip
RUN unzip vosk-model-spk-${RUVERSION}.zip \
   && mv vosk-model-spk-${RUVERSION} SpeakerModel \
   && rm -rf vosk-model-spk-${RUVERSION}.zip
WORKDIR /app
EXPOSE 80
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VoskApi.dll"]
