import { Component, createEffect, createSignal, For } from 'solid-js';
import { GetComments, ICommentModel, IFilterModel } from './components/data-service';
import AgGridSolid from '@ag-grid-community/solid';
import { ClientSideRowModelModule } from '@ag-grid-community/client-side-row-model';

import '@ag-grid-community/styles';

const App: Component = () => {
	const [comments, setComments] = createSignal(new Array<ICommentModel>());
	const defaultColDef = {
		flex: 1,
	  };
	const columnDefs = [
		{ field: 'id'},
		{ field: 'comment'},
		{ field: 'commentParsed'},
		{
			field: 'commentDate'
		}
	];
	
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
		// <For each={comments()}>
		// 	{(item, index) => (
		// 		<div><span>{item.comment} {item.commentDate && new Date(item.commentDate).toLocaleDateString()}</span><br /></div>
		// 	)}
		// </For>
		<div class='ag-theme-alpha' style={{ height: '100%' }}>
			<AgGridSolid
				columnDefs={columnDefs}
				rowData={comments()}
				defaultColDef={defaultColDef}
				modules={[ClientSideRowModelModule]}
			/>
		</div>
	);
};

export default App;