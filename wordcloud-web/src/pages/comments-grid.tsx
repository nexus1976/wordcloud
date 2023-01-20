import { createEffect, createSignal } from 'solid-js';
import { GetComments, ICommentModel, IFilterModel } from '../components/data-service';
import AgGridSolid from 'ag-grid-solid';
import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-alpine.css';

export function CommentsGrid(props: any) {
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
                console.log('setting comments', res);
                setComments(res);
            }	
        });
    };

    createEffect(() => hydrateComments());

    return (
        <div class='ag-theme-alpine' style={{ height: '100%' }}>
            <AgGridSolid
                columnDefs={[
                    { field: 'id' },
                    { field: 'comment' },
                    { field: 'commentParsed' },
                    { field: 'commentDate' }
                ]}
                rowData={comments()}
            />
        </div>
    );
}