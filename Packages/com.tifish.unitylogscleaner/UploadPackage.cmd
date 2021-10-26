@echo off
setlocal
set http_proxy=
set https_proxy=
call npm-cli-login -u niubi -p veryNiubi2021 -e niulianjun@kingsoft.com -r http://10.11.33.33:4873

call npm publish --registry http://10.11.33.33:4873
pause

endlocal
