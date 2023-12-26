<template>
    <div class="table-container" v-if="isInRole !== 'guest'">
        <table class="users-table" v-if="displayedUsers.length > 0">
            <thead>
                <tr>
                    <th>№</th>
                    <th>ФИО</th>
                    <th>Логин</th>
                    <th>Мастер</th>
                    <th>Начальник цеха</th>
                    <th>Администратор</th>

                </tr>
            </thead>
            <tbody>
                <!-- @click="selectRow(user.login)" -->
                <tr v-for="(user, index) in displayedUsers" :key="user.login"
                    :class="{ 'selected-row': selectedUser === user.login }" @click="selectRow(user.login)">
                    <td data-label="№">{{ index + 1 }}</td>
                    <td data-label="ФИО">{{ user.name + ' ' + user.surname + ' ' + user.middleName }}</td>
                    <td data-label="Логин">{{ user.login }}</td>
                    <td data-label="Мастер">
                        <img v-if="user.permission == 'foreman'" src="@/assets/settings_checkmark.svg" alt="Checkmark"
                            @click.stop="togglePermission(user, 'foreman', true)">
                        <img v-else src="@/assets/settings_empty.svg" alt="EmptyField"
                            @click.stop="togglePermission(user, 'foreman', false)">
                    </td>
                    <td data-label="Начальник цеха">
                        <img v-if="user.permission == 'shop manager'" src="@/assets/settings_checkmark.svg" alt="Checkmark"
                            @click.stop="togglePermission(user, 'shop manager', true)">
                        <img v-else src="@/assets/settings_empty.svg" alt="EmptyField"
                            @click.stop="togglePermission(user, 'shop manager', false)">
                    </td>
                    <td data-label="Администратор">
                        <img v-if="user.permission == 'admin'" src="@/assets/settings_checkmark.svg" alt="Checkmark"
                            @click.stop="togglePermission(user, 'admin', true)">
                        <img v-else src="@/assets/settings_empty.svg" alt="EmptyField"
                            @click.stop="togglePermission(user, 'admin', false)">
                    </td>
                </tr>

            </tbody>
        </table>
        <div class="pagination">
            <button v-for="pageNumber in totalPages" :key="pageNumber" @click="changePage(pageNumber)">
                {{ pageNumber }}
            </button>
        </div>
    </div>
</template>
  
<script lang="ts">
import { defineComponent } from 'vue';
import UsersInterface from "@/interfaces/UsersInterface";
import { User } from '@/interfaces/UsersInterface';
import auth from '@/authentificationService';

export default defineComponent({
    data() {
        return {
            users: [] as User[],
            currentPage: 1,
            itemsPerPage: 10,
            counter: 0,
            selectedUser: '',
        };
    },
    computed: {
        displayedUsers(): User[] {
            const startIndex = (this.currentPage - 1) * this.itemsPerPage;
            const endIndex = startIndex + this.itemsPerPage;

            const sortedUsers = this.users.slice().sort((a, b) => {
                // Сортировка по полю 'login'
                if (a.login < b.login) {
                    return -1;
                }
                if (a.login > b.login) {
                    return 1;
                }
                return 0;
            });

            return sortedUsers.slice(startIndex, endIndex);
        },
        totalPages(): number {
            return Math.ceil(this.users.length / this.itemsPerPage);
        },
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
        async togglePermission(user: User, permission: string, isActive: boolean) {
            console.log(user, permission, isActive);
            if (permission == user.permission) { return }
            else if (permission == 'foreman') { user.permission = 'foreman' }
            else if (permission == 'shop manager') { user.permission = 'shop manager' }
            else if (permission == 'admin') { user.permission = 'admin' }

            const res = await UsersInterface.put(user.login, user.permission);
            console.log(res.status);
        },

        getUsers() {
            UsersInterface.getAll().then(json => {
                this.users = json.data;
                console.log(this.users);
            });
        },
        changePage(pageNumber: number) {
            this.currentPage = pageNumber;
        },
        selectRow(login: string) {
            if (this.selectedUser == login) { this.selectedUser = ''; } else { this.selectedUser = login; }
            this.$emit('row-selected', this.selectedUser);
        },
    },
    mounted() {
        this.getUsers();
    },
});
</script>
  
<style scoped>
img {
    cursor: pointer;
}

.selected-row {
    background-color: var(--selected-event-type);
    /* Выбранный цвет фона строки */
    /* Цвет текста для лучшей видимости */
}

/* Existing styles for larger screens */
.users-table {
    width: 100%;
    margin-top: 10px;
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

.pagination {
    margin-top: 20px;
    display: flex;
    justify-content: center;
}

.pagination button {
    margin: 0 5px;
    padding: 5px 10px;
    border: 1px solid #ccc;
    border-radius: 5px;
    background-color: #f0f0f0;
    cursor: pointer;
}

.pagination button:hover {
    background-color: #e0e0e0;
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

    .pagination button {
        font-size: 12px;
        padding: 3px 8px;
        margin: 0 3px;
    }
}
</style>


<!-- Adjusted table structure for mobile -->
