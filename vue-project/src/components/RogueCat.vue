<script setup>
import  { RouterLink } from 'vue-router';
import { reactive, defineProps, computed } from 'vue';
import { onMounted } from 'vue';

const props = defineProps({
    frameWidth: 1024,
    frameHeight: 576,
    scale: 1
});

const state = reactive({
    audioContext: null
});

const initializAudioContext = () => {
    if (!state.audioContext) {
        audioContext = new (window.AudioContext || window.AudioContext)();
    }
    if (state.audioContext.state === 'suspended'){
        state.audioContext.resume();
    }
}

const frameSize = () => {
    const width = props.frameWidth * props.scale;
    const height = props.frameHeight * props.scale;
    return `w-[${width}px] max-w-full h-[${height}px]`;
};


onMounted(frameSize);
</script>

<template>
    <div>
        <section class="p-20 bg-grape h-full overflow-hidden">
            <h1 class="text-grape-light text-7xl">Rogue Cat</h1>
            <div @click="initializAudioContext">
                <div 
                class="mt-10 game bg-grape flex flex-row justify-center items-center h-full">
                    <iframe
                        class="overflow-hidden border-none bg-grape w-full max-w-[1024px] aspect-[16/9] rounded-lg shadow-lg"
                        src="https://i.simmer.io/@Chocolate_Axe/rogue-cat-epq-artefact"
                        >
                    </iframe>
                </div>
            </div>
            
        </section>
    </div>
</template>