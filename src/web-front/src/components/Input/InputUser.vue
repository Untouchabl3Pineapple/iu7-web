<template>
    <div class="table-container" v-if="isInRole !== 'guest'">
        <table class="users-table">
            <tbody>
                <tr>
                    <td data-label="№">0</td>
                    <td data-label="ФИО"> <b>Фамилия</b> <input v-model="surname" class="input-line"> <b>Имя</b> <input
                            v-model="name" class="input-line">
                        <b>Отчество</b>
                        <input v-model="middleName" class="input-line">
                    </td>
                    <td data-label="Данные для входа"> <b>Логин</b> <input v-model="login" class="input-line"> <b>Пароль</b>
                        <input v-model="password" class="input-line">
                    </td>
                    <td data-label="Мастер">
                        <img v-if="permission == 'foreman'" src="@/assets/settings_checkmark.svg" alt="Checkmark"
                            @click.stop="togglePermission('foreman')">
                        <img v-else src="@/assets/settings_empty.svg" alt="EmptyField"
                            @click.stop="togglePermission('foreman')">
                    </td>
                    <td data-label="Начальник цеха">
                        <img v-if="permission == 'shop manager'" src="@/assets/settings_checkmark.svg" alt="Checkmark"
                            @click.stop="togglePermission('shop manager')">
                        <img v-else src="@/assets/settings_empty.svg" alt="EmptyField"
                            @click.stop="togglePermission('shop manager')">
                    </td>
                    <td data-label="Администратор">
                        <img v-if="permission == 'admin'" src="@/assets/settings_checkmark.svg" alt="Checkmark"
                            @click.stop="togglePermission('admin')">
                        <img v-else src="@/assets/settings_empty.svg" alt="EmptyField"
                            @click.stop="togglePermission('admin')">
                    </td>
                </tr>

            </tbody>
        </table>
    </div>
</template>
  
<script lang="ts">
import { defineComponent } from 'vue';
import auth from '@/authentificationService';

export default defineComponent({
    name: "InputUser",
    props: {
        userData: Object // пропс для передачи данных из InputUser
    },
    data() {
        return {
            name: '',
            surname: '',
            middleName: '',

            login: '',
            password: '',

            permission: '',
        };
    },
    computed: {
        isInRole() {
            if (auth.getCurrentUser()) {
                console.log(auth.getCurrentUser().permission);
                return auth.getCurrentUser().permission;
            } else {
                auth.logout();
                return "guest";
            }
        },
    },
    methods: {
        togglePermission(permission: string) {
            console.log(this.name, this.surname, this.middleName, this.login, this.password);
            if (permission == this.permission) { this.permission = ''; return; }
            this.permission = permission;

            const filledUserData = {
                name: this.name,
                surname: this.surname,
                middleName: this.middleName,
                login: this.login,
                password: this.password,
                permission: this.permission,
            };

            this.$emit('input-change', filledUserData);
        }
    }
});
</script>
  
<style scoped>
@media screen and (min-width: 768px) {
    .users-table td:nth-child(1) {
        width: 5.3%;
        /* Примерный размер для первой колонки */
    }

    .users-table td:nth-child(2) {
        width: 34.98%;
        /* Примерный размер для второй колонки */
    }

    .users-table td:nth-child(3) {
        width: 13.6%;
        /* Примерный размер для второй колонки */
    }

    .users-table td:nth-child(4) {
        width: 10.1%;
        /* Примерный размер для второй колонки */
    }

    .users-table td:nth-child(5) {
        width: 18.2%;
        /* Примерный размер для второй колонки */
    }
}

.input-separator {
    display: inline-block;
    /* Регулируйте размер разделителя по вашему усмотрению */
}

.input-line {
    /* Ваши стили для поля ввода остаются без изменений */
    /* margin-top: 30px; */
    width: 100%;
    background: var(--white);
    border-radius: 20px;
    /* border-radius: 20px; */
    border: 1px solid var(--black);
    /* border: 2px solid transparent; */
    padding: 15px 20px;
    color: var(--black);
    box-sizing: border-box;
    /* padding-left: 100px; */
    padding-top: 12px;
    padding-bottom: 12px;
}

img {
    cursor: pointer;
}

/* Existing styles for larger screens */
.users-table {
    width: 100%;
    /* margin-top: 10px; */
    margin-bottom: 10px;
    border-collapse: collapse;
    border-radius: 3px;
    overflow: hidden;
}

.users-table th,
.users-table td {
    border: 2px solid #000000;
    padding: 12px;
    text-align: center;
}

.users-table th {
    background-color: #f2f2f2;
    font-weight: bold;
}

.users-table tbody tr:nth-child(even) {
    /* background-color: #f9f9f9; */
}

.users-table tbody tr:hover {
    /* background-color: #f5f5f5; */
}

.users-table tbody td {
    font-size: 14px;
}

.users-table tbody td:nth-child(1) {
    font-weight: bold;
}


/* Responsive styles for smaller screens (mobile devices) */
@media screen and (max-width: 768px) {

    .users-table th,
    .users-table td {
        padding: 30px;
        font-size: 12px;
    }

    .users-table th {
        display: none;
    }

    .users-table tbody td {
        display: block;
        text-align: right;
    }

    .users-table tbody td:before {
        content: attr(data-label);
        font-weight: bold;
        float: left;
        text-align: center;
    }

    .users-table tbody td:nth-child(even) {
        background-color: transparent;
    }

    .users-table tbody tr {
        margin-bottom: 10px;
        display: block;
        border: 1px solid #ccc;
        border-radius: 5px;
    }
}
</style>


<!-- Adjusted table structure for mobile -->
