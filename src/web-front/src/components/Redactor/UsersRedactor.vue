<template>
    <div>
        <InputUser :user-data="userData" @input-change="handleInputChange">
        </InputUser>
        <AddButton v-if="rowLogin == ''" @click="addUser()"> Добавить пользователя </AddButton>
        <DeleteButton v-if="rowLogin != ''" @click="deleteUser(rowLogin)"> Удалить пользователя </DeleteButton>
        <UsersTable @row-selected="getRowLogin"></UsersTable>

    </div>
</template>
  
<script lang="ts">
import { defineComponent } from 'vue';

import AddButton from "@/components/Buttons/AddButton.vue"
import DeleteButton from "@/components/Buttons/DeleteButton.vue"
import InputUser from "@/components/Input/InputUser.vue"
import UsersTable from '@/components/Tables/UsersTable.vue';
import UsersInterface from '@/interfaces/UsersInterface';

export default defineComponent({
    name: 'UsersRedactor',
    components: {
        UsersTable,
        DeleteButton,
        AddButton,
        InputUser,
    },

    data() {
        return {
            rowLogin: '',
            userData: {
                name: '',
                surname: '',
                middleName: '',
                login: '',
                password: '',
                permission: '',
            },
        }
    },
    methods: {
        handleInputChange(userData: {
            name: string;
            surname: string;
            middleName: string;
            login: string;
            password: string;
            permission: string;
        }) {

            this.userData = userData;
        },

        getRowLogin(login: string) {
            console.log(`Deleting row with login: ${login}`);
            this.rowLogin = login;
        },

        async deleteUser(login: string) {
            const res = await UsersInterface.deleteByLogin(login);
            console.log(res)
            this.$router.go(0);
        },

        async addUser() {
            console.log(this.userData);
            if (this.userData.login == '' || this.userData.password == '' || this.userData.permission == '') { return }
            const res = await UsersInterface.register(this.userData);
            console.log(res)
            this.$router.go(0);
        },
    },
});
</script>
  
  
<style scoped></style>