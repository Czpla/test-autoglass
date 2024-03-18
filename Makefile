# Pegar os argumentos passados para o make
ifeq (ef, $(firstword $(MAKECMDGOALS)))
  args := $(wordlist 2, $(words $(MAKECMDGOALS)), $(MAKECMDGOALS))
  $(eval $(args):;@true)
endif

all: build
	dotnet App.Infra/WebApi/bin/Release/net5.0/WebApi.dll

run:
	dotnet run --project App.Infra/WebApi/WebApi.csproj

watch:
	dotnet watch run --project App.Infra/WebApi/WebApi.csproj

build:
	dotnet publish -c Release App.Infra/WebApi/WebApi.csproj

tests:
	find . -name *.Test.csproj -type f | xargs -I {} dotnet test {}

# Help commands
ef:
	dotnet ef $(args) -c PostgresContext -p App.Infra/Data/Data.csproj -s App.Infra/WebApi/WebApi.csproj

clean:
	find . -name *.csproj -type f | xargs -I {} dotnet clean {}