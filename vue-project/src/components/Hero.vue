<script setup>
import { defineProps, ref } from 'vue';
import placeholderImage from '@/assets/img/placeholder.png'
import { onMounted } from 'vue';
import { cachedState } from '@/composables/store.js';
import axios from 'axios';

const props = defineProps({
    title: {
        type: String,
        default: 'Caitlin Thaeler'
    },
    subtitle: {
        type: String,
        default: 'Hello there! I\m a computer science student at the university of aberdeen'
    },
    portrait: {
        type: String,
        default: 'ui/portrait-no-bg.png'
    }
    
});

const imageUrl = ref(cachedState.portraitUrl || placeholderImage);

const fetchImage = async () => {
    if (!cachedState.portraitUrl)
    {
        console.log('fetching portrait')
        try {
        const encodedThumbnail = encodeURIComponent(props.portrait);
        // example request for image url: http://localhost:5283/api/caitlinthaeler/portfolio_content/image/sk8-run/thumbnail.png
        const response = await axios.get(`http://localhost:5283/api/caitlinthaeler/portfolio_content/image/${encodedThumbnail}`,);
        console.log(response)
        // Create a URL for the blob data
        cachedState.portraitUrl = response.data.imageUrl;
        imageUrl.value = cachedState.portraitUrl;
        console.log(imageUrl.value);
        } catch (error) {
            console.error("Error fetching image", error);
        }
    } else {
        imageUrl.value = cachedState.portraitUrl;
    }
    
}


onMounted(fetchImage);
</script>

<template>
    <div>
        <!-- Hero -->
        <section>
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
                <div class="flex-1 basis-2/3 flex-none">
                    <div class="flex justify-end h-full">
                        <div class="w-2/3 flex items-end">
                            <p class="my-4 text-xl text-plum">
                            {{ subtitle}}
                        </p>
                        </div>
                            
                        
                    </div>
                   
                </div>
                <div class="flex-1 flex justify-start items-center">
                    <img :src="imageUrl" alt="Embdedded image" class="w-40 h- object-cover rounded-full">
                </div>
                
            </div>
            <div class="flex flex-col gap-5 md:flex-row justify-center text-center text-sm">
                <div>
                    <button class="bg-lemon p-2">View Resume</button>
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
            <div>

            </div>
        </div>
        </section>
       
    </div>
</template>