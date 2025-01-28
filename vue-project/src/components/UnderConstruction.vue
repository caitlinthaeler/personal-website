<script setup>
import { RouterLink } from 'vue-router'
</script>

<template>
    <div>
        <section class="text-center flex flex-col justify-center items-center h-96 bg-grape text-grape-light">
            <i class="pi pi-hammer text-lemon text-7xl mb-5"></i>
            <h1 class="text-6xl font-bold mb-4">Page Under Construction</h1>
            <p class="text-xl mb-5">Content coming soon!</p>
            <RouterLink to="/" class="text-grape bg-lemon hover:bg-plum rounded-md px-3 py-2 mt-4"
            >Go Back</RouterLink>
        </section>
    </div>
</template>