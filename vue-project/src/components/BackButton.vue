<script setup>
import  { RouterLink } from 'vue-router';
</script>

<template>
    <div>
        <section>
            <div class="container m-auto py-6 px-6">
                <RouterLink to="#" @click.prevent="$router.back()" class="text-grape bg-lemon hover:bg-plum rounded-md px-3 py-2 mt-4"
            >Go Back</RouterLink>
            </div>
        </section>
    </div>
</template>