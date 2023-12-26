// import SquadInterface from "./interfaces/SquadInterface";
import UsersInterface from "@/interfaces/UsersInterface";
import AccountInterface from "@/interfaces/AccountInterface";

interface User {
    id: number,
    login: string,
    permission: string
}

export default {
    async login(login: string, password: string) {
        try {
            const result = await AccountInterface.login(login, password);
            console.log("LoginAuth:", result.status);
    
            if (result.status === 200) {
                const json = await UsersInterface.getByLogin(login);
                const data = json.data;
                console.log(data);
                localStorage.setItem('currentUser', JSON.stringify(data));
                console.log("currentUser: ", JSON.parse(String(localStorage.getItem("currentUser"))));
                return true;
            }
    
            return false;
        } catch (error) {
            console.error('Error during login:', error);
            return false;
        }
    },

    // async register(login: String, password: String) {
    //     const resUser = await UsersInterface.register(login, password);
    //     console.log("RegisterAuth:", resUser.status);

    //     if (resUser.status == 200) {
    //         const resSquad = await SquadInterface.addSquad(0, login + "Squad", 0);
    //         console.log("AddSquadAuth:", resSquad.status);
    //         return true;
    //     }

    //     return false;
    // },
    
    getCurrentUser() {
        return JSON.parse(String(localStorage.getItem("currentUser")));
    },
    
    async logout() {
        const result = await AccountInterface.logout();

        if (result.status == 200) {
            console.log("LogoutAuth");
            const user: User = {
                id: 0,
                login: "guest",
                permission: "guest"
            }
    
            localStorage.setItem('currentUser', JSON.stringify(user));
            console.log("currentUser: ", JSON.parse(String(localStorage.getItem("currentUser"))));
            return true;
        }

        return false;
    }
}