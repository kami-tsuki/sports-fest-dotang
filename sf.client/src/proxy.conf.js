const { env } = require('process');
const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
    env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'https://localhost:7149';
const PROXY_CONFIG = [
  {
    context: [
      "/api/Class",
       "/api/Class/bulk",
       "/api/Class/search",
       "/api/Class/count",
       "/api/Class/{id}",
       "/api/Class/export",
       "/api/Class/validate",
       "/api/Class/{id}/audit",
       "/api/User",
       "/api/User/bulk",
       "/api/User/search",
       "/api/User/count",
       "/api/User/{id}",
       "/api/User/export",
       "/api/User/validate",
       "/api/User/{id}/audit",
       "/api/Discipline",
       "/api/Discipline/bulk",
       "/api/Discipline/search",
       "/api/Discipline/count",
       "/api/Discipline/{id}",
       "/api/Discipline/export",
       "/api/Discipline/validate",
       "/api/Discipline/{id}/audit",
       "/api/Team",
       "/api/Team/bulk",
       "/api/Team/search",
       "/api/Team/count",
       "/api/Team/{id}",
       "/api/Team/export",
       "/api/Team/validate",
       "/api/Team/{id}/audit",

    ],
    target,
    secure: false
  }
]
module.exports = PROXY_CONFIG;
