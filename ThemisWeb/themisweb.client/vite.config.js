import { fileURLToPath, URL } from 'node:url';

import { defineConfig } from 'vite';
import plugin from '@vitejs/plugin-react';
import fs from 'fs';
import path from 'path';
import child_process from 'child_process';
import { env } from 'process';

const baseFolder =
    env.APPDATA !== undefined && env.APPDATA !== ''
        ? `${env.APPDATA}/ASP.NET/https`
        : `${env.HOME}/.aspnet/https`;

const certificateName = "themisweb.client";
const certFilePath = path.join(baseFolder, `${certificateName}.pem`);
const keyFilePath = path.join(baseFolder, `${certificateName}.key`);

if (!fs.existsSync(certFilePath) || !fs.existsSync(keyFilePath)) {
    if (0 !== child_process.spawnSync('dotnet', [
        'dev-certs',
        'https',
        '--export-path',
        certFilePath,
        '--format',
        'Pem',
        '--no-password',
    ], { stdio: 'inherit', }).status) {
        throw new Error("Could not create certificate.");
    }
}

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
    env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'https://localhost:7135';

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [plugin()],
    resolve: {
        alias: {
            '@': path.resolve(__dirname, "./src"),
        }
    }
    ,  
    server: {
        proxy: {
            '^/register': {
                target,
                secure: false
            },
            '^/login': {
                target,
                secure: false
            },
            '^/account/getsessioninfo': {
                target,
                secure: false
            },
            '^/group/getusergroups': {
                target,
                secure: false
            },
            '^/submittment': {
                target,
                secure: false
            },
            '^/group': {
                target,
                secure: false
            }, '^/group/getgroupinfo': {
                target,
                secure: false
            }, '^/users/getsearchusers': {
                target,
                secure: false
            },'^/account/logout': {
                target,
                secure: false
            }, '^/assignment': {
                target,
                secure: false
            },
            '^/user/usertexts': {
                target,
                secure: false
            }
        },
        port: 5173,
        https: {
            key: fs.readFileSync(keyFilePath),
            cert: fs.readFileSync(certFilePath),
        }
    }
})
