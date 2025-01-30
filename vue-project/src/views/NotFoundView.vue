<script setup>
import { RouterLink } from 'vue-router'
import GoBackButton from '@/components/BackButton.vue'
import ReturnHomeButton from '@/components/ReturnHomeButton.vue'
</script>

<template>
    <div>
        <section class="text-center flex flex-col justify-center items-center h-96 bg-grape text-lemon">
            <i class="pi pi-exclamation-triangle text-lemon text-7xl mb-5"></i>
            <h1 class="text-6xl font-bold mb-4">404 Not Found</h1>
            <p class="text-xl mb-5">This page does not exist</p>
            <div class="flex flex-row">
                <GoBackButton />
                <ReturnHomeButton />
            </div>
        </section>
    </div>
</template>