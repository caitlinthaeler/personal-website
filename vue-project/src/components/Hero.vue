<script setup>
import { defineProps } from 'vue';

defineProps({
    title: {
        type: String,
        default: 'Caitlin Thaeler\'s Portfolio'
    },
    subtitle: {
        type: String,
        default: 'I study computing science at the University of Aberdeen. '
    }
    
});
</script>

<template>
    <div>
        <!-- Hero -->
        <section class="bg-grape py-20 mb-4">
        <div
            class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 flex flex-col items-center"
        >
            <div class="text-center">
            <h1
                class="text-4xl font-extrabold text-grape-light sm:text-5xl md:text-6xl"
            >
                {{ title }}
            </h1>
            <p class="my-4 text-xl text-plum">
                {{ subtitle}}
            </p>
            </div>
        </div>
        </section>
    </div>
</template>