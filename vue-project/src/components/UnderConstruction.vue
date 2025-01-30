<script setup>
import { RouterLink } from 'vue-router'
import GoBackButton from '@/components/BackButton.vue'
import ReturnHomeButton from '@/components/ReturnHomeButton.vue'

const props = defineProps({
    title: {
        type: String,
        default: "Unknown Page"
    }
})
</script>

<template>
    <div>
        <h1 class="p-10 text-grape-light text-7xl">{{ title }}</h1>
        <section class="text-center flex flex-col justify-center items-center h-96 bg-grape text-lemon">
            
            <i class="pi pi-hammer text-lemon text-7xl mb-5"></i>
            <h1 class="text-6xl font-bold mb-4">Page Under Construction</h1>
            <p class="text-xl mb-5">Content coming soon!</p>
            <div class="flex flex-row">
                <GoBackButton />
                <ReturnHomeButton />
            </div>
            
        </section>
    </div>
</template>