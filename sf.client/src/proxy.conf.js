const { env } = require('process');
const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
    env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'https://localhost:5000';
const PROXY_CONFIG = [
  {
    context: [
      "/api/class",
       "/api/class/bulk",
       "/api/class/search",
       "/api/class/count",
       "/api/class/{id}",
       "/api/class/export",
       "/api/class/validate",
       "/api/class/{id}/audit",
       "/api/class/import",
       "/api/user",
       "/api/user/bulk",
       "/api/user/search",
       "/api/user/count",
       "/api/user/{id}",
       "/api/user/export",
       "/api/user/validate",
       "/api/user/{id}/audit",
       "/api/user/import",
       "/api/discipline",
       "/api/discipline/bulk",
       "/api/discipline/search",
       "/api/discipline/count",
       "/api/discipline/{id}",
       "/api/discipline/export",
       "/api/discipline/validate",
       "/api/discipline/{id}/audit",
       "/api/discipline/import",
       "/api/team",
       "/api/team/bulk",
       "/api/team/search",
       "/api/team/count",
       "/api/team/{id}",
       "/api/team/export",
       "/api/team/validate",
       "/api/team/{id}/audit",
       "/api/team/import",

    ],
    target,
    secure: false
  }
]
module.exports = PROXY_CONFIG;
