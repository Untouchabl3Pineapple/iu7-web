import axios from "axios";

axios.defaults.headers.common['Access-Control-Allow-Headers'] = '*';
axios.defaults.headers.common['Access-Control-Allow-Origin'] = '*';
axios.defaults.headers.common['Access-Control-Allow-Methods'] = '*';


const clientAccount = axios.create({
    baseURL: 'http://localhost:5000/api/v1/users',
    validateStatus: function (status: number) {
        return status < 500;
    }
});

export interface User {
    login: string,
    password: string,
    permission: string,
    name: string,
    surname: string,
    middleName: string
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
    async getByLogin(login: string) {
        return await this.execute('get', `/${login}`)
    },
    getAll() {
        return this.execute('get', '/');
    },

    deleteByLogin(login: string) {
        return this.execute('delete', `/${login}`)
    },

    put(login: string, permission: string) {
        return this.execute('put', `/${login}`, {permission});
    },

    register(userData: User) {
        return this.execute('post', '/register', userData);
    },
}
