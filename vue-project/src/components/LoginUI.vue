<script setup>
import { RouterLink, useRouter } from 'vue-router'
import { reactive, ref, defineProps, onMounted } from 'vue'
import axios from 'axios'
import { requestAuthorization } from '@/utils/authorization.js';
import logo from '@/assets/img/bond-unclicked.png'

const router = useRouter();

const loginForm = reactive({
    username: '',
    password: ''
});

async function OnLogin(){
    // authorize in backend
    try {
        const loginResponse = await requestAuthorization(loginForm.username, loginForm.password);
        if (loginResponse){
          // go to admin dashboard
            router.push('/admin/dashboard');
        } else {
          //deal with various errors
          console.error('incorrect login details');
        }
        
    } catch (error) {
        console.error('Error with login:', error);
    }
}

</script>

<template>
    <section class="bg-grape h-full overflow-hidden">
        <!-- home -->
        <div class="mx-auto max-w-7xl p-6 sm:p-5">
                <div class="flex flex-row h-auto items-center justify-between">
                    <div
                        class="flex flex-col md:flex-row w-full items-start md:items-stretch"
                    >
                        <!-- Logo -->
                        <RouterLink class="flex flex-shrink-0 items-center mr-4" to="/">
                        <img class="h-10 w-auto" :src="logo" alt="img" />
                        <span class="md:block text-lemon text-md font-bold ml-2"
                            >Caitlin Thaeler</span
                        >
                        </RouterLink>
                    </div>
                </div>
            </div>
        <div class="flex flex-col items-center justify-center px-6 py-8 mx-auto">

            <h1 class="text-grape-light text-7xl mb-10">Admin Page</h1>
            <!-- content -->
            <div class="bg-lemon rounded-lg shadow md:p-0 sm:max-w-md xl:p-0">
                <div class="p-6 space-y-6">
                    <h1 class="text-4xl text-center font-bold leading-tight tracking-tight text-grape">
                        Sign in to your account
                    </h1>
                    <form class="space-y-6" @submit.prevent="OnLogin">
                         <!-- login fields -->
                        <div class="flex flex-row space-x-3 items-center">
                            <label class="" for="username">Username</label>
                            <input class="bg-grape-light placeholder-plum rounded-lg p-1" type="text" id="username" v-model="loginForm.username" placeholder="Username">
                        </div>
                        <div class="flex flex-row space-x-3 items-center">
                            <label for="password">Password</label>
                            <input class="bg-grape-light placeholder-plum rounded-lg p-1" type="text" id="password" v-model="loginForm.password" placeholder="Password">
                            </div>
                        <!-- login button -->
                        <button type="submit" class="w-full text-lemon bg-grape hover:bg-plum rounded-lg text-sm px-5 py-2.5 text-center">
                            Login
                        </button>
                    </form>
                </div>
               
            </div>
        </div>
        
    </section>
</template>