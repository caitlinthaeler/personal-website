<script setup>
import { defineProps, ref } from 'vue';
import placeholderImage from '@/assets/img/placeholder.png'
import { onMounted } from 'vue';
import { cachedState } from '@/composables/store.js';
import { fetchImage } from '@/utils/fetchImage.js'
import axios from 'axios';

const props = defineProps({
    title: {
        type: String,
        default: 'Caitlin Thaeler'
    },
    subtitle: {
        type: String,
        default: ''
    },
    portrait: {
        type: String,
        // default: 'ui/portrait-no-bg.png'
        default: 'ui/caitlinpfp2024.png'
    }
    
});

const imageUrl = ref(cachedState.portraitUrl || placeholderImage);

const getImage = async () => {
    if (!cachedState.portraitUrl && props.portrait)
    {
        try {
        const response = await fetchImage(props.portrait);
        // Create a URL for the blob data
        cachedState.portraitUrl = response;
        imageUrl.value = response;
        } catch (error) {
            console.error("Error fetching image", error);
        }
    } else {
        imageUrl.value = cachedState.portraitUrl;
    }
    
}


onMounted(getImage);
</script>

<template>
    <div>
        <!-- Hero -->
        <section>
        <div
            class="max-w-7xl mx-auto flex flex-col gap-5"
        >
            <div class="text-center">
            <p class="text-center text-plum">
                *colors looks different with extensions like dark reader. I will work on this eventually.
            </p>
            <h1
                class="text-7xl font-extrabold text-grape-light"
            >
                {{ title }}
            </h1>
            </div>
            <div class="grid grid-cols-2">
                <div class="flex-1 flex-none">
                    <div class="flex justify-end h-full items-center">
                        <p class="my-4 text-xl text-plum">
                            
                            If you've got the <span class="text-plum-light">vision</span>,<br> I've got the <span class="text-plum-light">code</span>.
                        </p>
                    </div>
                </div>
                <div class="flex-1 flex-none ml-20">
                    <div class="flex justify-start h-full items-center">
                        <img :src="imageUrl" alt="Embdedded image" class="w-40 object-cover rounded-full">
                    </div>
                </div>
                <!-- <div class="flex-1 basis-1/3 flex-none">
                    <div class="flex justify-center h-full">
                        <img :src="imageUrl" alt="Embdedded image" class="w-40 object-cover rounded-full">
                    </div>
                </div> -->
                
            </div>
            <div class="flex flex-col gap-5 md:flex-row justify-center text-center text-sm">
                <div>
                    <RouterLink to="/resume" class="bg-lemon text-grape hover:bg-plum rounded-md px-3 py-2 mt-4"
                    >View Resume</RouterLink>
                </div>
               
                <!-- <div class="md:basis-1/3 md:flex-none">
                    <button class="flex items-center justify-center bg-lemon p-2">View Resume</button>
                </div> -->
                <!-- <div class="md_basis-2/3 md:flex-grow">
                   <button>Github</button>
                   <button>LinkedIn</button>
                   <button>Insta</button>
                   <button>Email</button>
                </div> -->
            </div>
            
        </div>
        </section>
       
    </div>
</template>