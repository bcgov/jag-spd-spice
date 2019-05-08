@echo off

echo Updating meta data

curl http://carla-spice-dev.pathfinder.bcgov/swagger/v1/swagger.json > carla-spice.swagger

echo Updating client

autorest --verbose --input-file=carla-spice.swagger --output-folder=.  --csharp --use-datetimeoffset --sync-methods=all --generate-empty-classes --override-client-name=CarlaClient  --namespace=Gov.Lclb.Cllb.Interfaces --preview  --add-credentials --debug
