<script setup lang="ts">
import type { Unit, Optional } from '@/types';
import { inject, onMounted, ref } from 'vue';
import UnitsService from '@/services/unitsService';
import { useRoute, useRouter } from 'vue-router';
import { handleApiResult } from '@/helpers/apiUtils';
import ConditionalContent from '@/components/ConditionalContent.vue';
import type IngredientTypesService from '@/services/ingredientTypesService';

const unitsService = inject('unitsService') as UnitsService;
const ingredientTypesService = inject('ingredientTypesService') as IngredientTypesService;

const unit = ref<Optional<Unit>>(null);
const errors = ref<string[]>([]);

const route = useRoute();
const router = useRouter();
const id = route.params.id.toString();

onMounted(async () => {
    await handleApiResult<Unit>({
        result: unitsService.findById(id),
        dataRef: unit,
        errorsRef: errors,
        router,
        fallbackRedirect: 'Units'
    });
    
    if (unit.value?.ingredientTypeId) {
        unit.value.ingredientType = (await ingredientTypesService.findById(unit.value.ingredientTypeId)).data!;
    }
});

const deleteUnit = async () => {
    await handleApiResult<Unit>({
        result: unitsService.delete(unit.value!.id!),
        errorsRef: errors,
        router,
        fallbackRedirect: 'Units',
        successRedirect: 'Units'
    });
};
</script>

<template>
    <h1>Delete</h1>

    <ConditionalContent :errors="errors" :expected-content="unit">
        <div>
            <h3>Are you sure you want to delete this?</h3>
            <div>
                <h4>Ingredient type</h4>
                <hr>

                <dl class="row">
                    <dt class="col-sm-2">
                        Name
                    </dt>
                    <dd class="col-sm-10">
                        {{ unit!.name }}
                    </dd>
                    <dt class="col-sm-2">
                        Abbreviation
                    </dt>
                    <dd class="col-sm-10">
                        {{ unit!.abbreviation }}
                    </dd>
                    <dt class="col-sm-2">
                        Unit multiplier
                    </dt>
                    <dd class="col-sm-10">
                        {{ unit!.unitMultiplier }}
                    </dd>
                    <dt class="col-sm-2">
                        Ingredient type
                    </dt>
                    <dd class="col-sm-10">
                        {{ unit!.ingredientType?.name }}
                    </dd>
                </dl>

                <form method="post">
                    <input @click.prevent="deleteUnit" type="submit" value="Delete" class="btn btn-danger"> |
                    <RouterLink :to="{name: 'Units'}">Back to List</RouterLink>
                </form>
            </div>
        </div>
    </ConditionalContent>
</template>

<style scoped></style>