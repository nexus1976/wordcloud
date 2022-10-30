import { Component, createEffect, createSignal, For } from 'solid-js';
import { GetComments, ICommentModel, IFilterModel } from './components/data-service';

const App: Component = () => {
	const [comments, setComments] = createSignal(new Array<ICommentModel>());
	
	const hydrateComments = () => {
		const filter: IFilterModel = {
			page: 1,
			pageSize: 100,
			fromDate: null,
			toDate: null
		};
		GetComments(filter).then(res => {
			if (res) {
				setComments(res);
			}	
		});
	};
	
	createEffect(() => hydrateComments());
	
	return (
		<For each={comments()}>
			{(item, index) => (
				<div><span>{item.comment} {item.commentDate && new Date(item.commentDate).toLocaleDateString()}</span><br /></div>
			)}
		</For>	
	);
};

export default App;