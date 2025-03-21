<script setup>
import  { useRouter } from 'vue-router';
import { removeAuthorization } from '@/utils/authorization.js';

const router = useRouter();

async function OnLogout(){
    // handle cookies in backend
    try {
        const logoutResponse = await removeAuthorization();
        if (logoutResponse){
          // go back to login page
            router.push('/admin/login');
        } else {
          //deal with various errors
          console.error('coudn\'t logout');
        }
        
    } catch (error) {
        console.error('Error with login:', error);
    }
}
</script>

<template>
    <div>
        <section>
            <div class="container m-auto py-6 px-6">
                <button type="submit" @click.prevent="OnLogout" class="text-lemon bg-grape hover:bg-plum rounded-lg text-sm px-5 py-2.5 text-center">
                    Logout
                </button>
            </div>
        </section>
    </div>
</template>