import axios from "axios";

axios.defaults.headers.common['Access-Control-Allow-Headers'] = '*';
axios.defaults.headers.common['Access-Control-Allow-Origin'] = '*';
axios.defaults.headers.common['Access-Control-Allow-Methods'] = '*';

const clientAccount = axios.create({
    baseURL: 'http://localhost:5000/api/v1/account',
    validateStatus: function (status: number) {
        return status < 500;
    }
});

export interface User {
    login: string,
    password: string,
    permission: string,
}

export default {
    execute(method: any, resource: any, data?: any, params?: any) {
        return clientAccount({
            method,
            url: resource,
            data,
            headers: {},
            params: params
        });
    },
    login(login: string, password: string) {
        return this.execute('post', '/login', {login, password});
    },
    logout() {
        return this.execute('get', '/logout');
    },
}
