import { createEffect, createSignal, For } from 'solid-js';
import { GetWordCounts, IWordCountModel } from '../components/data-service';

export function WordCounts(props: any) {
    const [wordCounts, setWordCounts] = createSignal(new Array<IWordCountModel>());

    const hydrateWordCounts = () => {
        GetWordCounts().then(res => {
            if (res) {
                setWordCounts(res);
            }
        });
    };

    createEffect(() => hydrateWordCounts());

    return (
        <>
            <For each={wordCounts()}>
                {(item, index) => (
                    <div><span>{item.word} {item.count}</span><br /></div>
                )}
            </For>
        </>
    )
}