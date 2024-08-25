const { env } = require('process');
const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
    env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'https://localhost:7149';
const PROXY_CONFIG = [
  {
    context: [
      "/api/v1/data/classes",
       "/api/v1/data/classes/search",
       "/api/v1/data/classes/{id}",
       "/api/v1/data/classes/count",
       "/api/v1/data/disciplines",
       "/api/v1/data/disciplines/search",
       "/api/v1/data/disciplines/{id}",
       "/api/v1/data/disciplines/count",
       "/api/v1/data/entries",
       "/api/v1/data/entries/search",
       "/api/v1/data/entries/{id}",
       "/api/v1/data/entries/count",
       "/api/v1/data/locations",
       "/api/v1/data/locations/search",
       "/api/v1/data/locations/{id}",
       "/api/v1/data/locations/count",
       "/api/v1/_/User",
       "/api/v1/_/User/search",
       "/api/v1/_/User/{id}",
       "/api/v1/_/User/count",

    ],
    target,
    secure: false
  }
]
module.exports = PROXY_CONFIG;
