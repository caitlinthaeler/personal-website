<script setup>
import { defineProps } from 'vue';
import placeholderImage from '@/assets/img/placeholder.png'

defineProps({
    title: {
        type: String,
        default: 'Caitlin Thaeler'
    },
    subtitle: {
        type: String,
        default: 'Coding enthusiast'
    }
    
});
</script>

<template>
    <div class="flex flex-col md:flex-row px-10 gap-5">
        <!-- Hero -->
        <section class="flex-1 py-20">
        <div
            class="max-w-7xl mx-auto flex flex-col gap-5"
        >
            <div class="text-center">
            <h1
                class="text-7xl font-extrabold text-grape-light"
            >
                {{ title }}
            </h1>
            </div>
            <div class="flex flex-row gap-5">
                <div class="flex-1">
                    <p class="my-4 text-xl text-plum">
                        {{ subtitle}}
                    </p>
                </div>
                <div class="flex-1">
                    <img :src="placeholderImage" alt="Embdedded image">
                </div>
                
            </div>
            <div class="flex flex-col gap-5 md:flex-row text-center">
                <div class="md:basis-1/3 md:flex-none">
                    <button class="flex items-center justify-center bg-lemon p-2">Resume</button>
                </div>
                <div class="md_basis-2/3 md:flex-grow">
                   <button>Github</button>
                   <button>LinkedIn</button>
                   <button>Insta</button>
                   <button>Email</button>
                </div>
            </div>
            <div>

            </div>
        </div>
        </section>
        <section class="flex-1 bg-lemon py-20">
            <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 flex flex-col items-center">

            </div>
        </section>
    </div>
</template>