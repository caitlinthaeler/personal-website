import './assets/css/main.css'
import 'primeicons/primeicons.css'
import PrimeVue from 'primevue/config'
import Toast from 'vue-toastification'
import 'vue-toastification/dist/index.css'
import router from './router'



import { createApp } from 'vue'
import App from './App.vue'

const app = createApp(App);


app.use(router);
app.use(Toast);
app.use(PrimeVue);

app.mount('#app')
