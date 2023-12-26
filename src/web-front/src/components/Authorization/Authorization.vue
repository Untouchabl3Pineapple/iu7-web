<template>
	<body class="container">

		<body class="authorization-container">
			<form class="form-column" @submit.prevent="onSubmit">
				<div class="form-row">
					<TextBlack class="text-black"> Логин </TextBlack>
					<InputAuth @login="setLogin" name="login" class="input-row" defaultText="" />
				</div>
				<div class="form-row">
					<TextBlack class="text-black"> Пароль </TextBlack>
					<InputAuth @password="setPassword" name="password" class="input-row" defaultText="" />
				</div>
				<InputButton> Войти </InputButton>
			</form>
		</body>
	</body>
</template>

<script lang="ts">
import { defineComponent } from 'vue'
import InputAuth from '@/components/Input/InputAuth.vue'
import TextBlack from "@/components/Text/TextBlack.vue"
import InputButton from "@/components/Buttons/InputButton.vue"

import auth from "@/authentificationService";
import router from "@/router";

export default defineComponent({
	name: "AuthorizatioN",
	components: {
		InputAuth,
		TextBlack,
		InputButton,
	},
	data() {
		return {
			login: '',
			password: '',
		}
	},
	methods: {
		async onSubmit() {
			console.log("Authorization:", this.login, this.password);

			if (this.login == '' || this.password == '') {
				this.$notify({
					title: "Ошибка",
					text: "Логин или пароль не введены",
				});
				return;
			}

			const result = await auth.login(this.login, this.password);

			if (result) {
				console.log("You are logged in")
				router.push("/");
			}
			else {
				console.log("Incorrect Data")

				this.$notify({
					title: "Ошибка",
					text: "Логин или пароль неправильные",
				});
			}
		},
		setLogin(login: string) {
			this.login = login;
		},
		setPassword(password: string) {
			this.password = password;
		},
	}
})
</script>

<style scoped>
.authorization-container {
	background: var(--white);
	box-shadow: 0px 0px 20px var(--white);
	border-radius: 20px;
	padding: 2%;
	color: var(--gray);
	width: 35%;
}

.form-column {
	display: flex;
	flex-direction: column;
	gap: 20px;
}

.form-row {
	display: flex;
	flex-direction: column;
	gap: 10px;
}

/* Adjust InputRow style */
.input-row {
	display: flex;
	flex-direction: row;
	align-items: center;
	gap: 10px;
}

/* Apply styles to TextBlack and InputRow components */
.text-black {
	font-size: var(--middle-text);
	color: var(--black);
	/* Change the color as per your design */
}

.input-row input {
	font-size: var(--middle-text);
	/* Additional styles for InputRow */
}
</style>